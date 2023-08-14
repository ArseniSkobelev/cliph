<script>
    import Reveal from "../../../../components/Reveal.svelte";

    export let data;
</script>

{#if data.data !== undefined && data.data.value !== undefined}
    <Reveal text={data.data.value} />
{:else}
    <span>Loading API key..</span>
{/if}