<script>
    import ChevronArrowBack from "./icons/ChevronArrowBack.svelte";

    let open = false;
</script>

<div
        class='{open ? "w-72" : "w-28"} duration-300 ease-in-out relative flex shadow-elevation flex-col h-full p-4 bg-theme-primary-accent text-theme-light-text'>
    <div class="flex w-full items-center justify-center">
        <h1 class="font-medium text-xl">cliph.</h1>
    </div>
    <ChevronArrowBack clickProp={() => open = !open} stroke={false} color="#333e4d"
                      className='{open ? "rotate-0" : "rotate-180"} duration-300 ease-in-out absolute top-16 -right-4 cursor-pointer z-20' />
</div>