import QtQuick 2.2
import QtQuick.Dialogs 1.1
import messageDialog 1.0

MessageDialog {
    title: "Answer the Question"
    icon: StandardIcon.Question
    text: "Yes or No?"
    detailedText: "Don't worry about it"
    standardButtons: StandardButton.Yes | StandardButton.No
    Component.onCompleted: visible = true
    onYes: { 
        console.log("Users says Yes")
        DialogTestModel.answer = "Yes"
    }
    onNo: console.log("Users says No")
    onRejected: console.log("aborted")
}