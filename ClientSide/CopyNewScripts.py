import shutil, sys, os
print('[WaveRP] Script by CzarAlex for updating clientside packages.')
print('---')
print('Initialization.')
minus_depth = lambda path, minus: '\\'.join(path.split('\\')[:len(path.split('\\'))-minus])  # minus_depth('C:\Folder1\folder', 1) -> 'C:\Folder1'
path_old, path_new = (minus_depth(sys.argv[0], 2) + cur for cur in ('\\client_packages\\cs_packages', '\\ClientSide'))
files_name = ('.cs', '.html', '.htm', '.css', '.js')  # valid script file extensions
folders_name = ('.vs', 'bin', 'obj')  # folders for scipping
updated = []
exit_exception = False  # if True then ends the script with except
def getDir(path, inj=''):
    files = [(inj + i, int(os.stat(f'{path}\\{i}').st_mtime)) for i in list(os.walk(path))[0][2] if i[i.find('.'):] in files_name]
    for folder in [i for i in list(os.walk(path))[0][1] if i not in folders_name]:
        for new_file in getDir(f'{path}\\{folder}', inj + folder + '\\'):
            files.append(new_file)
    return files
print('Preparation is complete.')
print('---')
print('Collecting old scripts.')
data_old = getDir(path_old)
if len(data_old): print('Old scripts collected.')
else: print('Old scripts not found.')
print('---')
print('Collecting new scripts.')
data_new = getDir(path_new)
if len(data_new):
	for file in data_new:
		if file[0] not in [i[0] for i in data_old]:
			print(f'Found new file: {file[0]}.')
			updated.append(file)
		else:
			if file[1] > [i for i in data_old if i[0] == file[0]][0][1]:
				print(f'Found updated file: {file[0]}.')
				updated.append(file)
	print('New scripts collected.')
	if len(updated):
		for file in updated:
			try:
				shutil.copy2(*(f'{cur}\\{file[0]}' for cur in (path_new, path_old)))
				print(f'File {file[0]} copied successfully.')
			except Exception as exc:
				print(f'[{exc}] Error while copying {file[0]} file.')
				if exit_exception: exit(0)
	else: print('All scripts already updated.')
else: print('New scripts not found.')