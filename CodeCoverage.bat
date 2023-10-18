@echo off 

set indexFile=%cd%"/moon-dev/CodeCoverage/Report/index.html" 

if exist %indexFile% (
    start %indexFile%
) else (
echo "Code coverage has not generate!"
) 
@echo on