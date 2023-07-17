<script>
    import SideBar from "./SideBar.svelte";
</script>

<div class="flex flex-row h-full font-lato">
    <!--    sidebar    -->
    <SideBar/>
    <div class="flex flex-col">
        <div>
            <!--      top-bar      -->
            <h2>TOPBAR</h2>
        </div>
        <div>
            <slot/>
        </div>
    </div>
</div>