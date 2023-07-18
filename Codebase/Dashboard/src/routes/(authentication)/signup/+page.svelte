<script lang="ts">
    import {enhance} from '$app/forms';
    import type {ActionData} from './$types';

    export let form: ActionData;
</script>

<div class="flex flex-col gap-y-8 w-10/12 md:w-4/12 lg:w-2/12">
    <div class="flex justify-center">
        <h1 class="text-4xl">Sign up</h1>
    </div>
    <form action="?/signup" method="post" class="flex flex-col gap-y-8" use:enhance>
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
            >
        </div>
        <button
                class="px-4 py-2 rounded bg-brand-500 text-white flex items-center justify-center font-semibold basic-animation hover:bg-brand-600">
            Signin
        </button>
    </form>
    {#if form?.error}
        <div>
            {form.error}
        </div>
    {/if}
</div>