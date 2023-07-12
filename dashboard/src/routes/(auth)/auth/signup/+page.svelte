<script lang="ts">
    //
    // -------------------- SIGNUP
    //

    import {enhance} from "$app/forms";

    import Button from "../../../../components/Button.svelte";

    import type { ActionData } from './$types';
    export let form: ActionData;
</script>

<form method="POST" action="?/register" use:enhance>
    <div class="flex flex-col gap-y-8">
        <div class="flex flex-col gap-y-2">
            <label for="email">
                Email
            </label>
            <input type="text" name="email" id="email" class="rounded w-full p-2 border"
                   placeholder="john.doe@gmail.com">
        </div>
        <div class="flex flex-col gap-y-2">
            <label for="email">
                Password
            </label>
            <input type="password" name="password" id="password" class="rounded w-full p-2 border"
                   placeholder="***********">
        </div>
        <Button Text="Signup" HasIcon={false} />
        <div class="flex flex-col gap-y-4 text-center items-center">
            <span class="text-sm text-[#999999]">OR</span>
            <hr class="border-[#999999] w-full">
            <a href="/auth/login" class="text-theme-primary-accent-500 text-sm">Click here to log into an existing
                account</a>
        </div>
    </div>
</form>

<!--{#if form?.error}-->
    <div
            class="fixed bottom-4 left-4 right-4 text-theme-light-text flex gap-x-4 flex-row justify-center items-center text-center z-50 bg-theme-danger-500 rounded-md">
        <div class="text-theme-light-text flex gap-x-4 p-4 flex-row justify-center items-center text-center w-full">
            <img src="/icons/icon-park-outline_attention.png" height="32" width="32" alt="Attention!">
            <span class="w-10/12">
            <!--{form.error}-->
            User with the provided username/email already existsadsasdhakjshfdlhjkaskdjfhlashdfkashdlkfh kashdflk
            sahdkfj hsakldfh kjsdhf lkashd jkahkf asdkadhfhlsfkh
        </span>
        </div>
    </div>
<!--{/if}-->