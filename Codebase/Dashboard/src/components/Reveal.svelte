<script lang="ts">
    export let text: string = "Some hidden data";

    let isRevealed: boolean = false;
</script>

<div class="flex flex-col gap-4">
    <div
            class="rounded-lg bg-brand-500 text-white flex flex-col gap-4 text-center w-96 justify-center transition-all duration-300 hover:bg-brand-700 items-center px-4 py-4 cursor-pointer"
            on:click={() => isRevealed = !isRevealed}>
        {#if isRevealed}
            <span>{text}</span>
        {:else}
            <span>The content is currently hidden. Please click the box to reveal the hidden information</span>
        {/if}
    </div>
    {#if isRevealed}
        <button
                class="rounded-lg px-4 py-2 flex justify-center items-center bg-brand-500 text-white transition-all duration-300 hover:bg-brand-700"
                on:click={() => navigator.clipboard.writeText(text)}>
            Copy to clipboard
        </button>
    {/if}
</div>