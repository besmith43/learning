export function ToggleQuickLinks(){
	if($('#btnQuickLinkSettingsIcon').hasClass('fa-unlock')){
		relockQuickLinkBar();
	} else {
		unlockQuickLinkBar();
	}
}

function unlockQuickLinkBar() {
	$('#btnQuickLinkSettingsIcon').addClass('fa-unlock');
	$('#btnQuickLinkSettingsIcon').removeClass('fa-cog');
	$('#btnQuickLinkSettingsIcon').prop('title', 'Lock');
	$('#btnQuickLinkSettings').addClass('btn-danger');
	$('.quickLink').off('click');
	$('.quickLink').on('click', function(e){
		e.preventDefault();
	});
	$('#cardQuickLinksFooter').slideToggle('slow');
}

function relockQuickLinkBar() {
	var strTilesInOrder = '';
	$('#quicklinkul li').each(function(){
		strTilesInOrder += $(this).prop('id').split('-')[1] + ',';
	});

	$('#cardQuickLinksFooter').slideToggle('slow');

	strTilesInOrder = strTilesInOrder.substring(0,strTilesInOrder.length -1);
	$('#btnQuickLinkSettingsIcon').addClass('fa-cog');
	$('#btnQuickLinkSettingsIcon').removeClass('fa-unlock');
	$('#btnQuickLinkSettingsIcon').prop('title', 'Unlock');
	$('#btnQuickLinkSettings').removeClass('btn-danger');
	
	$('#quicklinkul').removeClass('drag-container');
	$('#availablequicklinkul').removeClass('drag-container');
	$('.quickLink').on('click', function(e){
		window.open(this.getAttribute('href'),'_blank');
		return false;
	});
	$.getJSON(strUnprotectedAPILocation + 'updateQuickLinksLineupBySessionID.php', {
		strSessionID : strSessionID,
		strLineup    : strTilesInOrder
	}).fail(function (textStatus, error) {
		console.log('Request Failed: ' + jQuery(textStatus).text() + ', ' + jQuery(error).text());
	});
}

var drake = dragula();


