<script lang="ts">
    import {enhance} from '$app/forms';
    import type {ActionData, SubmitFunction} from './$types';
    import Button from "../../../components/Button.svelte";
    import Alert from "../../../components/Alert.svelte";

    export let form: ActionData;

    let isLoading = false;

    const createSession: SubmitFunction = (input) => {
        isLoading = true;

        return async ({update}) => {
            isLoading = false;
            await update();
        }
    }
</script>

<div class="flex flex-col gap-y-8 w-10/12 md:w-4/12">
    <div class="flex justify-center">
        <h1 class="text-4xl">Sign in</h1>
    </div>
    <form action="?/signin" method="post" class="flex flex-col gap-y-8" use:enhance={createSession}>
        <div class="flex flex-col gap-y-2">
            <label
                    for="email"
                    class="text-lg"
            >
                Email address</label>
            <input
                    name="email"
                    id="email"
                    type="email"
                    placeholder="john.doe@gmail.com"
                    class="p-2 border rounded border-dark-gray focus:outline-none focus:ring-2 focus:ring-brand-500 focus:border-brand-500"
                    minlength="6"
                    required
            >
        </div>
        <div class="flex flex-col gap-y-2">
            <label
                    for="password"
                    class="text-lg"
            >
                Password</label>
            <input
                    name="password"
                    id="password"
                    type="password"
                    placeholder="***********"
                    class="p-2 border rounded border-dark-gray focus:outline-none focus:ring-2 focus:ring-brand-500 focus:border-brand-500"
                    minlength="8"
                    required
            >
        </div>
        <Button text="Signin" {isLoading}/>
        <a class="flex justify-center mt-8" href="/signup">
            <span class="text-brand-500 basic-animation hover:text-brand-600 text-xl text-center">
                Don't have an account yet? <strong>Click here to sign up</strong>
            </span>
        </a>
    </form>
    {#if form?.error}
        <div>
            <Alert alertType="error" alertMessage={form.error}/>
        </div>
    {/if}
</div>