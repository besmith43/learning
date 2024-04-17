import QtQuick 2.9
import QtQuick.Controls 2.1

Item {

	Column {
		spacing: 40
		width: parent.width

		Text {
			text: "Home page"
		}

		Button {
    	    x: 50
    	    y: 50
    	    text: "Go Test"
    	    onClicked: loader.source = "test.qml"
    	}
	}
}