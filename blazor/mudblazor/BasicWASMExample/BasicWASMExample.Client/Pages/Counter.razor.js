export function ButtonClicked(btnID) {
    console.log('JS Button ID: ' + btnID);
    $('#'+ btnID).removeClass('btn-primary');
    $('#'+ btnID).addClass('btn-success');
}