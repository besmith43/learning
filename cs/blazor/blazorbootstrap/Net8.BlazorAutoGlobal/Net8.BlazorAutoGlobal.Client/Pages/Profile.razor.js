// export function ToggleProfilePanels() {
//     $('#ShowMoreButton').on('click', function () {
//         console.log('FirstPanel clicked');
//     });
// }


export function SlidePanels(){
    console.log("ShowMoreButton Clicked");
    console.log("Slide Toggle Panel 1");
    $("#panel1").slideToggle("slow");

    console.log("Slide Toggle Panel 2");
    $("#panel2").slideToggle("slow");
}