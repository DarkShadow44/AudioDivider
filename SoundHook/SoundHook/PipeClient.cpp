#include "allIncludes.h"

void PipeClientMessage(ClientMessage message)
{
	switch(message.action)
	{
	case 1:
		addLog("Switching to device.");
		// Copy string from temporary message into a new string
		LPWSTR str = (LPWSTR) &message.data[0];
		int strLen = wcslen(str) + 1;
		forceDeviceId = new wchar_t[strLen];
		memcpy(forceDeviceId, &message.data[0], strLen*sizeof(wchar_t));
		break;
	}
}

void PipeClientStart()
{
	Sleep(500);
	addLog("Hooking!");
	setHooks();
	addLog("Thread running.");
	addLog("Trying to connect...");
	bool clientRunning = true;
	bool loggedAfterPipeBroke = false;
	while(clientRunning)
	{
		HANDLE hPipe = CreateFile(pipeName, GENERIC_WRITE | GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);
		if(hPipe == INVALID_HANDLE_VALUE)
		{	
			if(!loggedAfterPipeBroke)
			{
				if(GetLastError() != ERROR_FILE_NOT_FOUND)
				{
					addLog("CreateFile failed: Server Closed.");
				}
				else
				{
					addLog("CreateFile failed: ", GetLastError());
				}

				if(GetLastError() == ERROR_ACCESS_DENIED)
					addLog("Access denied.");
			}

			Sleep(2000);
			loggedAfterPipeBroke = true;
			continue;
		}
		loggedAfterPipeBroke = false;
	
		addLog("Connected.");
		bool clientConnected = true;
		while(clientConnected)
		{
			ClientMessage message;
			DWORD bytesRead;

			int numErr = 0;
			int result;
			while((result = ReadFile(hPipe, &message, sizeof(message), &bytesRead, NULL)) == 0 && GetLastError() == ERROR_BROKEN_PIPE && numErr < 10) // Try to read for few times, then give up
			{
				numErr++;
				Sleep(10);
			}

			if(result == 0) // ReadFile failed
			{
				if(GetLastError() == ERROR_BROKEN_PIPE) // Probably server closed
				{
					Sleep(2000);
					clientConnected = false;
					addLog("Lost connection. Trying to connect again...");
				}
				else
				{
					addLog("ReadFile failed: ", GetLastError());
				}
			}
			else // ReadFile succeded
			{
				if(message.pid == GetCurrentProcessId()) // Messages are sent to all hooked processes, check if the message is destined to this process
				{
					addLog("Got message.");
					PipeClientMessage(message);
				}
				else
				{
					addLog("Got message (ignored).");
				}

				Sleep(50);
			}
		
		}
	}
	addLog("Client ended.");
}