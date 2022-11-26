import QtQuick 2.9
import QtQuick.Dialogs 1.0
import Test 1.0

FileDialog {
    id: fileDialog
    title: "Please choose a file"
    folder: shortcuts.home
    sidebarVisible: true
    onAccepted: {
        console.log("You chose: " + fileDialog.fileUrls)
        model.SetFile()
    }
    onRejected: {
        console.log("Canceled")
    }
    Component.onCompleted: visible = true

    FileModel {
        id: model
    }
}