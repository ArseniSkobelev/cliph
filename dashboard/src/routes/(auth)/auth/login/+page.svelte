<script>
    //
    // -------------------- LOGIN
    //

    import Button from "../../../../components/Button.svelte";
</script>

<div class="flex flex-col gap-y-8">
    <div class="flex flex-col gap-y-2">
        <label for="email">
            Email
        </label>
        <input type="text" id="email" class="rounded w-full p-2 border" placeholder="john.doe@gmail.com">
    </div>
    <div class="flex flex-col gap-y-2">
        <label for="email">
            Password
        </label>
        <input type="password" id="password" class="rounded w-full p-2 border" placeholder="***********">
    </div>
    <Button Text="Login" HasIcon={false} />
    <div class="flex flex-col gap-y-4 text-center items-center">
        <span class="text-sm text-[#999999]">OR</span>
        <hr class="border-[#999999] w-full">
        <a href="/auth/signup" class="text-theme-primary-accent-500 text-sm">Click here to create a new account</a>
    </div>
</div>