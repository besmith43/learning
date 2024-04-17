import 'zx/globals'

void async hello () {
	await $`ls -al`
}()
