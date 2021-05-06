// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function Toggle(el) {
    console.log("I'm clicked!");
    var sibling = el.parentNode.children[1];
    if (sibling.classList.length == 0) {
        sibling.style.display = 'none';
        sibling.classList.add('hidden');
    } else {
        sibling.style.display = 'block';
        sibling.classList.remove('hidden');
    }
}