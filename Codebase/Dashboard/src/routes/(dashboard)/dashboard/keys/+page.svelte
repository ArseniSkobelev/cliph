<script>
    export let data;
</script>

{#if data.data !== undefined && data.data.value !== undefined}
    <span>Your API key: {data.data.value}</span>
{:else}
    <span>Loading API key..</span>
{/if}