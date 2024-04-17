import-module /root/powershell_interface.dll

$add = test-samplecmdlet -operation add -a 2 -b 5
$sub = test-samplecmdlet -operation sub -a 8 -b 3
$mul = test-samplecmdlet -operation mul -a 2 -b 3
$div = test-samplecmdlet -operation div -a 6 -b 2

write-error("add: 2 + 5 = " + $add)
write-error("sub: 8 - 3 = " + $sub)
write-error("mul: 2 * 3 = " + $mul)
write-error("div: 6 / 2 = " + $div)
