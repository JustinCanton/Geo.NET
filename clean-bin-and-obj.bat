@Echo Off
FOR /d /r .\ %%d IN (*) DO (
	@IF EXIST "%%d" (
		pushd %%d
		FOR /d /r %%t IN (bin\, obj\) DO (
			@IF EXIST "%%t" (
				ECHO "Removing %%t"
				rd /s /q "%%t"
			)
		)
		popd
	)
)

pause