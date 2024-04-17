export function ToggleQuickLinks(){
	if($('#btnQuickLinkSettingsIcon').hasClass('fa-unlock')){
		relockQuickLinkBar();
	} else {
		unlockQuickLinkBar();
	}
}

function unlockQuickLinkBar() {
	// $('#btnQuickLinkSettingsIcon').addClass('fa-unlock');
	// $('#btnQuickLinkSettingsIcon').removeClass('fa-cog');
	// $('#btnQuickLinkSettingsIcon').prop('title', 'Lock');
	// $('#btnQuickLinkSettings').addClass('btn-danger');
	// $('.quickLink').off('click');
	// $('.quickLink').on('click', function(e){
		// e.preventDefault();
	// });
	// $('#cardQuickLinksFooter').slideToggle('slow'); // this may be useful later

	console.log('called unlockQuickLinkBar');
	$('.quicklink-item').off('click');
	$('.quicklink-item').on('click', function(e){
		e.preventDefault();
	});


	// $('#btnToggle').addClass('btn-danger');
}

function relockQuickLinkBar() {
	// var strTilesInOrder = '';
	// $('#quicklinkul li').each(function(){
	// 	strTilesInOrder += $(this).prop('id').split('-')[1] + ',';
	// });

	// $('#cardQuickLinksFooter').slideToggle('slow');

	// strTilesInOrder = strTilesInOrder.substring(0,strTilesInOrder.length -1);
	// $('#btnQuickLinkSettingsIcon').addClass('fa-cog');
	// $('#btnQuickLinkSettingsIcon').removeClass('fa-unlock');
	// $('#btnQuickLinkSettingsIcon').prop('title', 'Unlock');
	// $('#btnQuickLinkSettings').removeClass('btn-danger');
	
	// $('#quicklinkul').removeClass('drag-container');
	// $('#availablequicklinkul').removeClass('drag-container');
	// $('.quickLink').on('click', function(e){
	// 	window.open(this.getAttribute('href'),'_blank');
	// 	return false;
	// });
	// $.getJSON(strUnprotectedAPILocation + 'updateQuickLinksLineupBySessionID.php', {
	// 	strSessionID : strSessionID,
	// 	strLineup    : strTilesInOrder
	// }).fail(function (textStatus, error) {
	// 	console.log('Request Failed: ' + jQuery(textStatus).text() + ', ' + jQuery(error).text());
	// });

	console.log('called relockQuickLinkBar');

	// TODO: this isn't quite working yet
	// designed to add on click event back to it
	$('.quicklink-item').on('click', function(e){
		window.open(this.getAttribute('href'),'_blank');
		return false;
	});

	// this is causing a problem where the hover remains red
	// $('#btnToggle').removeClass('btn-danger');
}

var drake = dragula();


