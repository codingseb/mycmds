#!/usr/bin/python
import sys
import os

if len(sys.argv) == 1:
	print 'alias v: 1.0.0'
	print 'alias command need 1 or 2 arguments.'
	print 'alias aliasname --> set the aliasname as alias of the current directory.'
	print 'alias aliasname specificdirectory --> set the aliasname as alias of directory path specified asspecificdirectory.'
	print 'Example : type alias pf86 "C:\Program files (x86)" then if you type just the command pf86 you will be set on the C:\Program files (x86) directory'
elif len(sys.argv) > 1:
	alias = sys.argv[1]
	print 'alias : ' + alias
	path = os.getcwd()
	if len(sys.argv) > 2:
		path = sys.argv[2]
	print 'path : ' + path
	root = os.path.splitdrive(path)[0]
	
	bat_file = open(os.path.dirname(__file__) + '\\alias\\' + alias + '.bat', 'w')
	
	bat_file.write('@echo off\n')
	bat_file.write(root + '\n')
	bat_file.write('cd "' + path + '"\n')
	
	bat_file.close()