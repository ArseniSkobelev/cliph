import {SECRET_AUTH_API_URI, SECRET_CSCA_HEADER_NAME, SECRET_CSCA_TOKEN} from "$env/static/private";

export async function DELETE({ request, cookies }) {
    const data = await request.json();
    const authToken = cookies.get("AuthorizationToken");

    const requestUri = `${SECRET_AUTH_API_URI}/api/v1/user`;

    const apiResponse = await fetch(requestUri, {
        method: "DELETE",
        headers: {
            [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
            Authorization: authToken,
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email: data.email
        })
    });

    const responseOptions: ResponseInit = {
        status: 418
    }
    return new Response('teapot', responseOptions);
}