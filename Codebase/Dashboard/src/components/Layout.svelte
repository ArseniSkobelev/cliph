<script>
    import SideBar from "./SideBar.svelte";
    import TopBar from "./TopBar.svelte";
</script>

<div class="flex flex-row h-full font-lato">
    <SideBar />
    <div class="flex flex-col">
        <div>
            <TopBar/>
        </div>
        <div class="px-8">
            <slot/>
        </div>
    </div>
</div>