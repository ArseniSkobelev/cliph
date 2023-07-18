import type { Actions } from "@sveltejs/kit";
import { fail, redirect } from "@sveltejs/kit";
import {
  SECRET_AUTH_API_URI,
  SECRET_CSCA_HEADER_NAME,
  SECRET_CSCA_TOKEN,
} from "$env/static/private";
import type { Response, JwtResponse } from "$lib";
import { setAuthToken } from "$lib/helpers";

export const actions: Actions = {
  signin: async ({ cookies, request }) => {
    // get the data from the form and extract named values; email, password
    const formData = Object.fromEntries(await request.formData());
    const { email, password } = formData;

    // validate username and password
    if (!email || !password)
      return fail(400, {
        error:
          "Email or password is missing! Please enter valid values into the presented fields.",
      });

    // CSCA - Cross Service Communication Authentication token. Cliph's way to ensure that admin users are created
    // strictly from authentication services.

    // make the request and handle response
    const response = await fetch(`${SECRET_AUTH_API_URI}/api/v1/auth/session`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        [SECRET_CSCA_HEADER_NAME]: SECRET_CSCA_TOKEN,
      },
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    });

    const responseBody: Response = await response.json();

    if (!responseBody.success)
      return fail(response.status, {
        error: responseBody.message,
      });

    const newUserData: JwtResponse = responseBody.data;

    setAuthToken({ cookies: cookies, token: newUserData.jwt });

    // redirect user to the dashboard
    throw redirect(302, "/");
  },
};
