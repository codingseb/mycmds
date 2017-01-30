#!/usr/bin/python
import sys
import os

my_cmds_path = os.path.dirname(__file__) + '\\mycmds'

if len(sys.argv) == 1:
	print 'ncmd v: 1.0.0'
	print 'ncmd command need 1 argument that is the command name that will be used for invoking the script.'
else:
	file_type=raw_input('Do you want only a .bat file type [b] or a python .py file type [p] ? : ')
	
	if file_type == 'p' or file_type == 'b':
		if not os.path.exists(my_cmds_path):
			os.makedirs(my_cmds_path)
		
		bat_file_name = my_cmds_path + '\\' + sys.argv[1] + '.bat'
		py_file_name = my_cmds_path + '\\' + sys.argv[1] + '.py'
		bat_file = open(bat_file_name, 'a')
		
		if file_type == 'p':
			bat_file.write('@echo off\n')
			bat_file.write('SET script_dir=%~dp0\n')
			bat_file.write('python %script_dir%' + sys.argv[1] + '.py %*\n')
			
		bat_file.close()
		
		os.system('npp ' + bat_file_name)
		if file_type == 'p':
			os.system('npp ' + py_file_name)
		
	else:
		print 'File type not know'
	
	