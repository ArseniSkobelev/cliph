import type { Actions } from './$types';
import { fail, redirect } from "@sveltejs/kit";
import { SECRET_AUTHENTICATION_API_ENDPOINT, SECRET_CSCA_KEY } from "$env/static/private";

interface UserRequest {
    email: string,
    password: string
}

interface ApiResponse {
    success: boolean,
    message: string,
    data: any | undefined
}

export const actions: Actions = {
    register: async ({cookies, request}) => {
        const formData = Object.fromEntries(await request.formData());
        const {email, password} = formData;

        let requestUrl = `${SECRET_AUTHENTICATION_API_ENDPOINT}/api/v1/auth/account`;
        let requestMethod = "POST";

        let userRequest: UserRequest = {
            email: email.toString(),
            password: password.toString()
        }

        const requestInfo: RequestInfo = new Request(requestUrl, {
            method: requestMethod,
            headers: {
                'Content-Type': "application/json",
                'x-cliph-cross-service-authentication': SECRET_CSCA_KEY,
            },
            body: JSON.stringify(userRequest)
        })

        let response;

        await fetch(requestInfo)
            .then(res => res.json())
            .then(res => {
                response = res as ApiResponse;
            })

        let error = "Unable to create a new user";

        if(response == undefined)
            return fail(500, {
                error
            })

        if(response['success'] == false)
        {
            let error = response['message'];

            return fail(500, {
                error
            })
        }

        // throw redirect(302, '/auth/login');
    }
}