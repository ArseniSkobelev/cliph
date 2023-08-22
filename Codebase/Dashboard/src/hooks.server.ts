import { redirect } from "@sveltejs/kit";
import {
  SECRET_AUTH_API_URI,
  SECRET_CSCA_HEADER_NAME,
  SECRET_CSCA_TOKEN,
} from "$env/static/private";
import type { Response } from "$lib";

/** @type {import('@sveltejs/kit').Handle} */
export async function handle({ event, resolve }) {
  const requestedPath = event.url.pathname;
  const cookies = event.cookies;
  const authToken = cookies.get("AuthorizationToken");

  if (requestedPath.includes("/dashboard")) {
    if (authToken == null) throw redirect(302, "/signup");
    
    const requestUrl = `${SECRET_AUTH_API_URI}/api/v1/user`;

    const apiResponse = await fetch(requestUrl, {
      method: "GET",
      headers: {
        [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
        Authorization: authToken,
      },
    });

    if (apiResponse.status != 200) throw redirect(302, "/signup");

    const responseBody: Response = await apiResponse.json();

    if (!responseBody.success) throw redirect(302, "/signup");
  }

  if(requestedPath == "/logout")
  {
    event.cookies.delete("AuthorizationToken");
    throw redirect(302, '/signup');
  }

  if(requestedPath == "/")
    throw redirect(302, '/dashboard');

  return await resolve(event);
}
