import QtQuick 2.9
import QtQuick.Controls 2.1

Item {

	Column {
		spacing: 40
		width: parent.width

		Text {
			text: "Test Page"
		}

		Button {
    	    x: 50
    	    y: 50
    	    text: "Go back to Main"
    	    onClicked: loader.source = "home.qml"
    	}
	}
}