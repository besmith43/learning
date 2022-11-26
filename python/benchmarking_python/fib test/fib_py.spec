# -*- mode: python -*-

block_cipher = None


a = Analysis(['fib.py'],
             pathex=['/mnt/d/CloudStation/Development/python_projects/personal_code/benchmarking_python/fib test'],
             binaries=[],
             datas=[],
             hiddenimports=[],
             hookspath=[],
             runtime_hooks=[],
             excludes=[],
             win_no_prefer_redirects=False,
             win_private_assemblies=False,
             cipher=block_cipher)
pyz = PYZ(a.pure, a.zipped_data,
             cipher=block_cipher)
exe = EXE(pyz,
          a.scripts,
          a.binaries,
          a.zipfiles,
          a.datas,
          name='fib_py',
          debug=False,
          strip=False,
          upx=True,
          runtime_tmpdir=None,
          console=False )
