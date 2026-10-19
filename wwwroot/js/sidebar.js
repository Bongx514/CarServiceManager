    (() => {
        const sidebar = document.getElementById("adminSidebar");
        const backdrop = document.getElementById("adminSidebarBackdrop");
        const openButton = document.getElementById("openAdminSidebar");
        const closeButton = document.getElementById("closeAdminSidebar");
        const mainContainer = document.querySelector(".main-container");

        if (!sidebar || !backdrop || !openButton || !closeButton || !mainContainer) return;

        const desktop = window.matchMedia("(min-width: 1024px)");

        const openSidebar = () => {
            if (desktop.matches) return;

            sidebar.classList.remove("-translate-x-full");
            backdrop.classList.remove("hidden");
            document.body.classList.add("overflow-hidden");
            openButton.setAttribute("aria-expanded", "true");
        };

        const closeSidebar = () => {
            if (desktop.matches) return;

            sidebar.classList.add("-translate-x-full");
            backdrop.classList.add("hidden");
            document.body.classList.remove("overflow-hidden");
            openButton.setAttribute("aria-expanded", "false");
        };

        const updateSidebar = () => {
            if (desktop.matches) {
                sidebar.classList.remove("-translate-x-full");
                backdrop.classList.add("hidden");
                document.body.classList.remove("overflow-hidden");
                openButton.setAttribute("aria-expanded", "true");
                mainContainer.classList.add("ml-[250px]");
            } else {
                sidebar.classList.add("-translate-x-full");
                backdrop.classList.add("hidden");
                document.body.classList.remove("overflow-hidden");
                openButton.setAttribute("aria-expanded", "false");
                mainContainer.classList.remove("ml-[250px]");
            }
        };

        openButton.addEventListener("click", openSidebar);
        closeButton.addEventListener("click", closeSidebar);
        backdrop.addEventListener("click", closeSidebar);

        document.addEventListener("keydown", event => {
            if (event.key === "Escape") closeSidebar();
        });

        desktop.addEventListener("change", updateSidebar);

        updateSidebar();
    })();