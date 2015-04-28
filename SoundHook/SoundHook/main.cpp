#include "allIncludes.h"

#pragma warning(disable: 4482)

int PipeBufferSize = 1000;
LPCWSTR pipeName = L"\\\\.\\pipe\\SoundInjectController";
LPWSTR forceDeviceId = 0; // The current device that should be used instead of the selected device

std::string LogPath = "";

std::string GetTimeStamp()
{
	time_t rawTime;
	struct tm * currentTime;
	time(&rawTime);
	currentTime = localtime(&rawTime);

	char buffer[500];
	sprintf_s(buffer, "[%.2d:%.2d:%.2d] (Client): ", currentTime->tm_hour, currentTime->tm_min, currentTime->tm_sec);
	return std::string(buffer);
}

void addLog(std::string text)
{
	if(LogPath.empty())
		return;
	std::fstream log;
	log.open (LogPath + "AudioDivider.log", std::fstream::out | std::fstream::app);

	log << GetTimeStamp() << text << std::endl;

	log.close();
}

void addLog(std::string text, int data)
{
	if(LogPath.empty())
		return;
	std::fstream log;
	log.open (LogPath + "AudioDivider.log", std::fstream::out | std::fstream::app);

	log << GetTimeStamp() << text << data << std::endl;

	log.close();
}

std::string GetLogPath()
{
	HKEY hKey;
	if(0 != RegOpenKeyExW(HKEY_CURRENT_USER, L"SOFTWARE\\AudioDivider", 0, KEY_READ, &hKey))
		return "";

	char buffer[500];
	DWORD length;
	if(0 != RegQueryValueExA(hKey, "Path", 0, NULL, (LPBYTE)buffer, &length))
		return "";
	return std::string(buffer);
}




void* GetDevice_originalAddress;
void* GetDefaultAudioEndpoint_originalAddress;

typedef HRESULT(_stdcall* GetDevicePtr)(void* _this, LPCWSTR pwstrId, IMMDevice **ppDevice);
typedef HRESULT(_stdcall* GetDefaultAudioEndpointPtr)(void* _this, EDataFlow dataFlow, ERole role, IMMDevice **ppDevice);


HRESULT _stdcall GetDefaultAudioEndpoint_Hooked(void* _this, EDataFlow dataFlow, ERole role, IMMDevice **ppDevice)
{
	if(forceDeviceId == 0) // If not set to any device just let the program use what it wants
	{
		GetDefaultAudioEndpointPtr GetDefaultAudioEndpoint = (GetDefaultAudioEndpointPtr)GetDefaultAudioEndpoint_originalAddress;
		HRESULT result = GetDefaultAudioEndpoint(_this, dataFlow, role, ppDevice);
		return result;
	}
	else
	{
		GetDevicePtr GetDevice = (GetDevicePtr)GetDevice_originalAddress;
		HRESULT result = GetDevice(_this, forceDeviceId, ppDevice);
		return result;
	}
}

HRESULT _stdcall GetDevice_Hooked(void* _this, LPCWSTR pwstrId, IMMDevice **ppDevice)
{
	if(forceDeviceId == 0)  // If not set to any device just let the program use what it wants
	{
		GetDevicePtr GetDevice = (GetDevicePtr)GetDevice_originalAddress;
		HRESULT result = GetDevice(_this, pwstrId, ppDevice);
		return result;
	}
	else
	{
		GetDevicePtr GetDevice = (GetDevicePtr)GetDevice_originalAddress;
		HRESULT result = GetDevice(_this, forceDeviceId, ppDevice);
		return result;
	}
}

// Sets a hook for a COM interface function by patching the vTable
void hookCOM(IUnknown *pInterface, void* newMethod, void**oldMethod, int offset)
{
	DWORD oldProtect;
	void** vTable = *((void***)pInterface); // Get the vTable, it's the first member of the interface
	VirtualProtect(&vTable[offset], sizeof(void*),PAGE_EXECUTE_READWRITE, &oldProtect); // Make the vtable writable, it can be readonly since you are not supposed to write there
	*oldMethod = vTable[offset]; // Backup method
	vTable[offset] = newMethod; // Set the hook
}

void setHooks()
{
	const CLSID CLSID_MMDeviceEnumerator = __uuidof(MMDeviceEnumerator);
	const IID IID_IMMDeviceEnumerator = __uuidof(IMMDeviceEnumerator);
	const IID IID_IAudioClient = __uuidof(IAudioClient);
	const IID IID_IAudioRenderClient = __uuidof(IAudioRenderClient);
	const IID IID_IAudioSessionManager = __uuidof(IAudioSessionManager);
	const IID IID_IAudioSessionManager2 = __uuidof(IAudioSessionManager2);
	const IID IID_IAudioSessionControl2 = __uuidof(IAudioSessionControl2);
	
	IMMDeviceEnumerator* deviceEnumerator;
	CoInitializeEx(NULL, COINIT_MULTITHREADED);
	HRESULT instance = CoCreateInstance(CLSID_MMDeviceEnumerator, NULL, CLSCTX_ALL, IID_IMMDeviceEnumerator, (void**)&deviceEnumerator);

	hookCOM(deviceEnumerator, &GetDefaultAudioEndpoint_Hooked, &GetDefaultAudioEndpoint_originalAddress, 4);
	hookCOM(deviceEnumerator, &GetDevice_Hooked, &GetDevice_originalAddress, 5);
}

#include <Aclapi.h>

extern "C" _declspec(dllexport) BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	switch(fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		LogPath = GetLogPath();
		addLog("Injected Dll.");
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)PipeClientStart, (LPVOID)0, 0, NULL);
		break;
	default:
		break;
	}
	return TRUE;
}