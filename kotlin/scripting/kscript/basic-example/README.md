# KScript

this tool is nice, but requires 1.9.24 as it hasn't been upgraded to support 2024's release of kotlin v2.0 as is detailed [here](https://github.com/kscripting/kscript/issues/421).
The solution to allow this is to run the following command:

```bash
	brew unlink kotlin && brew link kotlin@1.9.24  && rm -rf ~/Library/Caches/kscript/ && kscript 'println("hello world")'
```



