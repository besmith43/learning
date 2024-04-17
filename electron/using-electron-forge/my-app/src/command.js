const { exec } = require("child_process");

function run_command() {
    admin_account = document.getElementById("adminaccount");
    admin_pw = document.getElementById("adminpw");
    user_account = document.getElementById("useraccount");
    user_pw = document.getElementById("userpw");

    exec(`sysadminctl -adminUser \"${admin_account.value}\" -padminPassword \"${admin_pw.value}\" -secureTokenOn \"${user_account.value}\" -password \"${user_pw.value}\"`);
}