#include <string>
#include <vector>
#include <iostream>
#include <fstream>
//#include "easyhook.h"
#include <Windows.h>
#include <Mmdeviceapi.h>
#include <Audioclient.h>
#include <FunctionDiscoveryKeys_devpkey.h>
#include <Audiopolicy.h>
#include <string.h>
#include <wchar.h>
#include <time.h>

void addLog(std::string text);
void addLog(std::string text, int data);
void PipeClientStart();
void setHooks();

extern int PipeBufferSize;
extern LPCWSTR pipeName;
extern LPWSTR forceDeviceId; // The current device that should be used instead of the selected device

#pragma pack(push, 1)
struct ClientMessage
{
	int pid;
	int action;
	char data[200];
	int dataLen;
};
#pragma pack(pop)