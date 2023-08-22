import {SECRET_AUTH_API_URI, SECRET_CSCA_HEADER_NAME, SECRET_CSCA_TOKEN} from "$env/static/private";

export async function DELETE({ request, cookies }) {
    let data;
    
    try {
        data = await request.json();
    }
    catch {

    }
    
    const authToken = cookies.get("AuthorizationToken");
    
    const requestUri = `${SECRET_AUTH_API_URI}/api/v1/user`;

    let apiRequestOptions = {
        method: "DELETE",
        headers: {
            [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
            Authorization: authToken,
            "Content-Type": "application/json"
        },
        body: !data ? null : JSON.stringify({email: data.email})
    };

    let apiResponse = await fetch(requestUri, apiRequestOptions);

    let responseOptions: ResponseInit = {
        status: apiResponse.status
    }

    return new Response('', responseOptions);
}