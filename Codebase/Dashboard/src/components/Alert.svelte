<script lang="ts">
    export let alertType: 'info' | 'error' | 'warning' | 'success' = 'info';
    export let alertMessage: string = "This is an alert";
    let timeout: number = 1000;

    let isShown: boolean = true;

    const handleTick = () => {
        if (timeout === 1)
            hideAlert();

        timeout--;
    }

    const hideAlert = () => {
        clearInterval(tickInterval);
        isShown = false;
    }

    const tickInterval = setInterval(handleTick, 1);
</script>

{#if isShown}
    <div
            class='{alertType === "error" ? "bg-error-100 rounded-lg text-error-600"
                    : alertType ==="warning" ? "bg-warning-100 rounded-lg text-warning-600"
                    : alertType === "success" ? "bg-success-100 rounded-lg text-success-600"
                    : "bg-secondary-100 rounded-lg text-secondary-600"}
            flex text-lg cursor-pointer flex-col gap-x-4 justify-center'
            on:click={hideAlert}>
        <div class='{alertType === "error" ? "bg-error-600"
                    : alertType ==="warning" ? "bg-warning-600"
                    : alertType === "success" ? "bg-success-600"
                    : "bg-secondary-600"}
            relative top-0 left-0 h-1 rounded-l-lg rounded-r-lg'
            style='width: {timeout/10}%'></div>
        <div class="px-4 py-2 text-center">
            <span><strong>{alertMessage}</strong></span>
        </div>
    </div>
{/if}