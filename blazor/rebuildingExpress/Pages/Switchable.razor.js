console.log("Switchable.razor.js loaded");


$(document).on('click', '#switchTextButton', function() {
	$('#SwitchableParagraph').text('This text was set by jQuery');
})
