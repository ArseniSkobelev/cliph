<script lang="ts">
    import ChevronArrowBack from "./icons/ChevronArrowBack.svelte";
    import CliphLogo from "./icons/CliphLogo.png";

    import {NavigationLinks} from "../navigation.ts";
    import Button from "./Button.svelte";

    let open = true;
</script>

<div
        class='{open ? "w-96" : "w-32"} duration-300 flex flex-col ease-in-out shadow-elevation h-full p-5 pt-7 bg-theme-primary-accent-800 text-theme-light-text'>
    <div class="relative">
        <ChevronArrowBack clickProp={() => open = !open} stroke={false}
                          color={"#999999"}
                          className='{open ? "rotate-0" : "rotate-180"} duration-300 ease-in-out absolute top-20 -right-9 cursor-pointer z-20' />
    </div>
    <div class="flex gap-x-4 items-center">
        <img src="{CliphLogo}" width="56" height="56" alt="Logo" class="cursor-pointer">
        <div class="flex flex-col">
            <h1
                    class='{!open ? "scale-0 opacity-0 w-0" : "scale-100 opacity-100"} font-medium duration-300 text-2xl origin-left'>Cliph</h1>
        </div>
    </div>
    <ul class="pt-6 flex flex-col gap-2 justify-center">
        {#each NavigationLinks as navLink}
            <li>
                <a href={navLink.path} class='{navLink.gap ? "mt-8" : "mt-2"} gap-x-4 h-12 flex flex-row p-2
                    cursor-pointer hover:bg-theme-primary-accent-500 items-center
                    rounded-md duration-300'>
                    <img src={navLink.imagePath} alt="" height="32" width="32">
                    <span
                            class='{!open ? "scale-0 opacity-0 w-0" : "scale-100 opacity-100"} origin-left duration-300 text-sm md:text-base'>{navLink.title}</span>
                </a>
            </li>
        {/each}
    </ul>
    <div class="mt-auto w-full gap-y-4 flex-col flex items-center justify-center">
        <span class="text-sm text-[#d3d3d3]">Logout</span>
        <hr class="border-[#d3d3d3] w-full">
        <Button Text="Logout" IconPath="/icons/material-symbols_logout.png" IsPrimary={false} HasIcon={false}
                FillWidth={true}
                Open={open} />
    </div>
</div>