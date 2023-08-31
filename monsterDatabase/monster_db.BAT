echo off

sqlcmd -S localhost -E -i monster_db.sql

rem server is local host

ECHO .
ECHO if no errors appear DB was created
PAUSE