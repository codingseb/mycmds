#!/usr/bin/python
import sys
import os

my_cmds_path = os.path.dirname(__file__) + '\\mycmds'

if len(sys.argv) == 1:
	print 'ncmd v: 1.0.0'
	print 'ncmd command need 1 argument that is the command name that will be used for invoking the script.'
else:
	file_type=raw_input('Do you want only a .bat file type [b] or a python .py file type [p] or a C# file type [c] ? : ')
	
	if file_type == 'p' or file_type == 'c' or file_type == 'b':
		if not os.path.exists(my_cmds_path):
			os.makedirs(my_cmds_path)
		
		bat_file_name = my_cmds_path + '\\' + sys.argv[1] + '.bat'
		py_file_name = my_cmds_path + '\\' + sys.argv[1] + '.py'
		cs_file_name = my_cmds_path + '\\' + sys.argv[1] + '.cs'
		mode = 'w'
		if file_type == 'b':
			mode = 'a'
		bat_file = open(bat_file_name, mode)
		
		if file_type == 'p':
			bat_file.write('@echo off\n')
			bat_file.write('SET script_dir=%~dp0\n')
			bat_file.write('python %script_dir%' + sys.argv[1] + '.py %*\n')
		elif file_type == 'c':
			bat_file.write('@echo off\n')
			bat_file.write('SET script_dir=%~dp0\n')
			bat_file.write('cscs %script_dir%' + sys.argv[1] + '.cs %* | tail -n +4\n')
			
		bat_file.close()
		
		os.system('npp ' + bat_file_name)
		if file_type == 'p':
			os.system('npp ' + py_file_name)
		elif file_type == 'c':
			if not os.path.isfile(cs_file_name):
				cs_file = open(cs_file_name, 'w')
				cs_file.write('using System;\n')
				cs_file.write('using System.Windows.Forms;\n')
				cs_file.write('\n')
				cs_file.write('class Script\n')
				cs_file.write('{\n')
				cs_file.write('	[STAThread]\n')
				cs_file.write('	static public void Main(string[] args)\n')
				cs_file.write('	{\n')
				cs_file.write('		MessageBox.Show("Just a test!");\n')
				cs_file.write('\n')
				cs_file.write('		for (int i = 0; i < args.Length; i++)\n')
				cs_file.write('		{\n')
				cs_file.write('			Console.WriteLine(args[i]);\n')
				cs_file.write('		}\n')
				cs_file.write('	}\n')
				cs_file.write('}\n')
				cs_file.write('\n')
				cs_file.close()
			os.system('npp ' + cs_file_name)
			
			
	else:
		print 'File type not know'
	
	