import {SECRET_AUTH_API_URI, SECRET_CSCA_HEADER_NAME, SECRET_CSCA_TOKEN} from "$env/static/private";
import type {Response} from "$lib";

export async function load({ cookies }) {
    const authToken = cookies.get("AuthorizationToken");
    const apiUrl = `${SECRET_AUTH_API_URI}/api/v1/stats/users`;

    const apiResponse = await fetch(apiUrl, {
        method: "GET",
        headers: {
            [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
            Authorization: authToken,
        },
    })

    const parsedApiResponse: Response = await apiResponse.json();

    if(!parsedApiResponse.success)
        return {
            error: {
                message: "Unable to fetch the users associated with you API key"
            }
        }

    return {
        users: parsedApiResponse.data
    };
}
