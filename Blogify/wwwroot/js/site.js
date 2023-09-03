function changeNavBarBackground() {
    const header = document.querySelector('.header');
    const navBar = document.getElementById('nav-bar');
    const headerRect = header.getBoundingClientRect();
    const headerBottom = headerRect.bottom;

    if (window.scrollY > headerBottom) {
        navBar.style.backgroundColor = '#000000';
    } else {
        navBar.style.backgroundColor = 'transparent'; 
    }
}

// Attach the function to the scroll event
if (window.location.pathname == '/') {
    window.addEventListener('scroll', changeNavBarBackground);
} else {
    document.getElementById('nav-bar').style.backgroundColor = 'black';
    document.querySelector('body').style.paddingTop = document.getElementById('nav-bar').offsetHeight.toString() + 'px';
}

$(".scroll-top").on("click", function() {
    $(window).scrollTop(0);
});

function adjustTextareaHeight(textarea) {
    textarea.style.height = 'auto';
    textarea.style.height = `${textarea.scrollHeight}px`; 
}

const inputs = document.querySelectorAll('#dynamicInput');

inputs.forEach((input) => {
    input.addEventListener('input', () => {
        adjustTextareaHeight(input);
    });
});
