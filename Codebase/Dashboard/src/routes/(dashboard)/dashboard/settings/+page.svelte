<script lang="ts">
    import Button from "../../../../components/Button.svelte";
    import {goto, invalidateAll} from "$app/navigation";

    const deleteAccount = async () => {
        const isSure =
            confirm("Are you sure that you want to delete your admin account?\n" +
                "All of your managed users will be deleted in cascade");

        if(!isSure)
            return;

        const deleteResult = await fetch('/api/v1/users', {
            method: "DELETE"
        });

        invalidateAll();
        goto("/");
    }
</script>

<Button text="Delete account" isDanger={true} on:click={() => deleteAccount()} />