import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import AsyncTest 1.0

ApplicationWindow {
    id: window
    width: 360
    height: 520
    visible: true

    Column {
        x: 10
        y: 10
        spacing: 40
        width: parent.width

        Button {
            text: 'Change Text Value'
            onClicked: {
                var task = model.runTask('This is a test')
                Net.await(task, function(result) {
                    message.text = result
                })
            }
        }

        Text {
            id: message
        }

        TestModel {
            id: model
        }
    }
}