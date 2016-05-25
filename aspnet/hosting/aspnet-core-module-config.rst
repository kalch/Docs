.. _apppool:

ASP.NET Core Module Configuration Reference
=============================================

By `Rick Anderson`_

The ASP.NET Core Module:

- Is an IIS Module and requires IIS 8 or higher
- Performs process management of HTTP listeners. This can be any process that can listen on a port for HTTP requests (for example: Tomcat, Jetty, Node.exe, and Ruby)
- Proxys requests to the process that it manages

.. contents:: Sections:
  :local:
  :depth: 1

Configuration Attributes
^^^^^^^^^^^^^^^^^^^^^^^^^

+---------------------------+----------------------------------------------------+
| Attribute                 |     Description                                    |
+===========================+====================================================+
| processPath               | | Required string attribute.                       |
|                           | |                                                  |
|                           | | Path to the executable or script that will launch|
|                           | | a process listening for HTTP requests.           |
|                           | | Relative paths are supported. If the path        |
|                           | | begins with '.' the path is considered to be     |
|                           | | relative to the site root.                       |
|                           | |                                                  |
|                           | | There is no default value.                       |
+---------------------------+----------------------------------------------------+
| arguments                 | | Optional string attribute.                       |
|                           | |                                                  |
|                           | | Arguments to the executable or script            |
|                           | | specified in  **processPath**   .                |
|                           | |                                                  |
|                           | | There is no default value.                       |
+---------------------------+----------------------------------------------------+
| startupTimeLimit          | | Optional integer attribute.                      |
|                           | |                                                  |
|                           | | Duration in seconds for which the                |
|                           | | the handler will wait for the executable or      |
|                           | | script to start a process listening on           |
|                           | | the port. If this time limit is exceeded,        |
|                           | | the handler will kill the process and try to     |
|                           | | launch it again **startupRetryCount** times.     |
|                           | |                                                  |
|                           | | The default value is 10.                         |
+---------------------------+----------------------------------------------------+
| startupRetryCount         | | Optional integer attribute.                      |
|                           | |                                                  |
|                           | | The number of times the handler will try to      |
|                           | | launch the process specified in **processPath**. |
|                           | | See **startupTimeLimit** for details.            |
|                           | |                                                  |
|                           | | The default value is 10.                         |
+---------------------------+----------------------------------------------------+
| rapidFailsPerMinute       | | Optional integer attribute.                      |
|                           | |                                                  |
|                           | | Specifies the number of times the process        |
|                           | | specified in **processPath** is allowed to crash |
|                           | | per minute. If this limit is exceeded,           |
|                           | | the handler will stop launching the              |
|                           | | process for the remainder of the minute.         |
|                           | |                                                  |
|                           | | The default value is 10.                         |
+---------------------------+----------------------------------------------------+
| requestTimeout            | | Optional timespan  attribute.                    |
|                           | |                                                  |
|                           | | Specifies the duration for which the             |
|                           | | ASP.NET Core module will wait for a response     |
|                           | | from the process listening on                    |
|                           | | %HTTP_PLATFORM_PORT%.                            |
|                           | |                                                  |
|                           | | The default value is "00:02:00".                 |
+---------------------------+----------------------------------------------------+
| stdoutLogEnabled          | | Optional Boolean  attribute.                     |
|                           | |                                                  |
|                           | | If true, **stdout** and **stderr** for the       |
|                           | | process specified in **processPath** will be     |
|                           | | redirected to the file specified in              |
|                           | | **stdoutLogFile**.                               |
|                           | |                                                  |
|                           | | The default value is false.                      |
+---------------------------+----------------------------------------------------+
| stdoutLogFile             | | Optional string attribute.                       |
|                           | |                                                  |
|                           | | Specifies the relative or absolute file path for |
|                           | | which **stdout** and **stderr** from the process |
|                           | | specified in **processPath** will be logged.     |
|                           | | Relative paths are relative to the root of the   |
|                           | | site. Any path starting with '.' will be         |
|                           | | relative to the site root and all other paths    |
|                           | | will be treated as absolute paths.               |
|                           | |                                                  |
|                           | | The default value is ``httpplatform-stdout``.    |
+---------------------------+----------------------------------------------------+
| forwardWindowsAuthToken   | | True or False.                                   |
|                           | |                                                  |
|                           | | If  true, the token will be forwarded to the     |
|                           | | child process listening on %HTTP_PLATFORM_PORT%  |
|                           | | as a header 'X-IIS-WindowsAuthToken' per request.|
|                           | | It is the responsibility of that process to call |
|                           | | CloseHandle on this token per request.           |
|                           | |                                                  |
|                           | | The default value is false.                      |
+---------------------------+----------------------------------------------------+

Child Elements
^^^^^^^^^^^^^^^

+---------------------------+----------------------------------------------------+
| Attribute                 |     Description                                    |
+===========================+====================================================+
| environmentVariables      | | Configures **environmentVariables** collection   |
|                           | | for the process specified in **processPath**.    |
+---------------------------+----------------------------------------------------+
| recycleOnFileChange       | | Optional string attribute.                       |
|                           | |                                                  |
|                           | | Arguments to the executable or script            |
|                           | | specified in  **processPath**                    |
|                           | |                                                  |
|                           | | There is no default value.                       |
+---------------------------+----------------------------------------------------+

