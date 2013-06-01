Task runner for Windows
=======================

Usage
-----

* Create job package zip files that contain a file called manifest.json
* Start command line process with an argument pointing to the directory to watch for job packages

Job Package Manifest
--------------------

>{
>    "JobName": "sample",
>    "Executable": "Notepad.exe",
>    "CronExpression": "0 08 14 ? * *"
>}

Note that if you're running a custom job (ie a program you wrote), everything required to run it should be in the zip file.


HTTP Interface
--------------

TaskBoss has a simple HTTP interface that can list and run jobs.  

* /list will return a list of the names of all scheduled jobs
* /run/{job_name} will trigger immediate execution of the job named {job_name}