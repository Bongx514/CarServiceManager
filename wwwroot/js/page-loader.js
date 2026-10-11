(function () {

    function showLoader() {
        const loader = document.getElementById("page-loader");

        if (!loader) return;

        loader.classList.remove("hidden");
        loader.classList.remove("pointer-events-none");

        // Force browser to apply display before opacity transition
        requestAnimationFrame(function () {
            loader.classList.remove("opacity-0");
            loader.classList.add("opacity-100");
        });
    }


    function hideLoader() {
        const loader = document.getElementById("page-loader");

        if (!loader) return;

        loader.classList.remove("opacity-100");
        loader.classList.add("opacity-0");
        loader.classList.add("pointer-events-none");

        setTimeout(function () {
            loader.classList.add("hidden");
        }, 300);
    }


    /*
     * PAGE INITIAL LOAD
     */

    document.addEventListener("DOMContentLoaded", function () {
        hideLoader();
    });

    window.addEventListener("load", function () {
        hideLoader();
    });

    window.addEventListener("pageshow", function () {
        hideLoader();
    });


    /*
     * LINK CLICKS
     */

    document.addEventListener("click", function (event) {

        const link = event.target.closest("a");

        if (!link) return;


        // Don't show loader for:
        // Ctrl + click
        // Cmd + click
        // Shift + click
        // Alt + click
        if (
            event.ctrlKey ||
            event.metaKey ||
            event.shiftKey ||
            event.altKey
        ) {
            return;
        }


        // Don't show loader for new tabs/windows
        if (link.target === "_blank") {
            return;
        }


        // Don't show loader for downloads
        if (link.hasAttribute("download")) {
            return;
        }


        const href = link.getAttribute("href");

        // No href
        if (!href) {
            return;
        }


        // Don't show loader for anchors
        if (href.startsWith("#")) {
            return;
        }


        // Don't show loader for javascript links
        if (href.startsWith("javascript:")) {
            return;
        }


        // Don't show loader for mail/tel links
        if (
            href.startsWith("mailto:") ||
            href.startsWith("tel:")
        ) {
            return;
        }


        // Don't show loader if clicking the current page
        if (
            link.href === window.location.href ||
            link.href === window.location.href + "#"
        ) {
            return;
        }


        showLoader();

    });


    /*
     * FORM SUBMISSION
     */

    document.addEventListener("submit", function (event) {

        const form = event.target;

        if (!form) return;


        // Don't show loader if JavaScript has cancelled
        // the submission.
        if (event.defaultPrevented) {
            return;
        }


        // Don't show loader for forms that explicitly
        // opt out.
        if (form.hasAttribute("data-no-loader")) {
            return;
        }


        showLoader();

    });


    /*
     * BROWSER BACK / FORWARD
     */

    window.addEventListener("popstate", function () {
        showLoader();
    });


})();
