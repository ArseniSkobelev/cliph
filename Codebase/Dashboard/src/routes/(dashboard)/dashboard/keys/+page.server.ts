import {SECRET_AUTH_API_URI, SECRET_CSCA_HEADER_NAME, SECRET_CSCA_TOKEN} from "$env/static/private";
import type {Response} from "$lib";
import {redirect} from "@sveltejs/kit";

/** @type {import('./$types').PageServerLoad} */
export async function load({ cookies, params }) {
    const requestUrl = `${SECRET_AUTH_API_URI}/api/v1/apikey`;

    const apiResponse = await fetch(requestUrl, {
        method: 'GET',
        headers: {
            [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
            "Authorization": cookies.get("AuthorizationToken")
        }
    })

    const deserializedResult: Response = await apiResponse.json();

    if(apiResponse.status === 401) {
        event.cookies.delete("AuthorizationToken");
        throw redirect(302, '/signup');
    }

    if(!deserializedResult.success)
        return {
            error: "Unable to fetch your API key from the server",
            data: undefined
        };

    return {
        error: undefined,
        data: deserializedResult.data
    };
}