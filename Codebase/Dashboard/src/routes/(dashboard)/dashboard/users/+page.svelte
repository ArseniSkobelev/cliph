<script lang="ts">
    import Alert from "../../../../components/Alert.svelte";
    import { invalidateAll } from "$app/navigation";

    const deleteUser = async (userEmail: string) => {
        const isSure = confirm(`Are you sure that you want to delete ${userEmail}?`)

        if(!isSure)
            return;
        
        console.log(`deleting ${userEmail}..`);

        // send DELETE request to api with userEmail, authToken and CSCA
        const deleteResult = await fetch('/api/v1/users', {
            method: "DELETE",
            body: JSON.stringify({
                email: userEmail
            })
        });

        if(deleteResult.status != 500)
           invalidateAll();
    }
    
    export let data;
</script>

<div class="flex flex-col gap-y-2">
    <span>Click any of the users to delete their account</span>

    {#if data.error != undefined && data.error.message != undefined}
        <Alert alertType="error" alertMessage={data.error.message} />
    {/if}


    {#if data.users === undefined}
        <span>Unable to fetch your managed users</span>
    {/if}

    {#each data.users as user}
        <div class="flex px-4 py-2 rounded-lg bg-brand-100 cursor-pointer" on:click={() => deleteUser(user.email)}>
            <span>{user.email}</span>
        </div>
    {:else}
        <span>No managed users found</span>
    {/each}
</div>