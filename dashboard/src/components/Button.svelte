<script lang="ts">
    export let Text: string = "Button";
    export let IconPath: string;
    export let IsDanger: boolean = false;
    export let IsPrimary: boolean = true;
    export let FillWidth: boolean = false;
    export let ClassName: string;
    export let Open: boolean = false;
    export let HasIcon: boolean = false;
    export let OnClick = () => {}
</script>

<button on:click={OnClick}
        class='{ClassName} {Open ? "justify-start gap-x-2" : "justify-center"} px-2 py-2 flex duration-300 flex-row font-medium items-center text-sm md:text-base
        rounded-md {FillWidth ? "w-full justify-center"
        : ""}
        {IsDanger ?
"bg-theme-danger-500" :
IsPrimary ?
"bg-theme-primary-accent-600 hover:bg-theme-primary-accent-700 text-theme-light-text" :
"bg-theme-secondary-accent-600 text-theme-light-text hover:bg-theme-secondary-accent-700"}'>
    {#if HasIcon}
        <img src={IconPath} width="32" height="32" alt="Icon">
    {:else}
        <span class="">{Text}</span>
    {/if}
</button>