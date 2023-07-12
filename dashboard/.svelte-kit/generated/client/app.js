export { matchers } from './matchers.js';

export const nodes = [
	() => import('./nodes/0'),
	() => import('./nodes/1'),
	() => import('./nodes/2'),
	() => import('./nodes/3'),
	() => import('./nodes/4'),
	() => import('./nodes/5'),
	() => import('./nodes/6')
];

export const server_loads = [];

export const dictionary = {
		"/(dashboard)": [6,[3]],
		"/(auth)/auth/login": [4,[2]],
		"/(auth)/auth/signup": [~5,[2]]
	};

export const hooks = {
	handleError: (({ error }) => { console.error(error) }),
};

export { default as root } from '../root.svelte';