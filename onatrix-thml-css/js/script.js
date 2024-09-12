
document.querySelector('.search-icon').addEventListener('click', function(event) {
    event.stopPropagation();
    document.querySelector('.search-input').classList.toggle('show');
});


document.addEventListener('click', function(event) {
    const searchContainer = document.querySelector('.search-container');
    const searchInput = document.querySelector('.search-input');
    

    if (!searchContainer.contains(event.target)) {
        searchInput.classList.remove('show');
    }
});


﻿document.addEventListener('DOMContentLoaded', function () {
    const mobileButton = document.querySelector('.btn-mobile');
    const mobileMenu = document.getElementById('menu');
    const menuOverlay = document.getElementById('menu-overlay');
    const menuLinks = document.querySelectorAll('.menu-link');
   
    const hideMenuAndOverlay = (menu) => {
        menu.classList.remove('show-menu');
        menuOverlay.classList.remove('show-overlay');
    };

    mobileButton.addEventListener('click', function () {
        mobileMenu.classList.toggle('show-menu');
        menuOverlay.classList.toggle('show-overlay');
    });

    menuLinks.forEach(link => {
        link.addEventListener('click', () => hideMenuAndOverlay(mobileMenu)); 
    });
   
    const checkScreenSize = () => {
        if (window.innerWidth >= 1200) {
            hideMenuAndOverlay(mobileMenu); 
        }
    };

    const throttle = (func, limit) => {
        let inThrottle;
        return function () {
            const args = arguments;
            const context = this;
            if (!inThrottle) {
                func.apply(context, args);
                inThrottle = true;
                setTimeout(() => inThrottle = false, limit);
            }
        };
    };

    window.addEventListener('resize', throttle(checkScreenSize, 200));
    checkScreenSize();


});



