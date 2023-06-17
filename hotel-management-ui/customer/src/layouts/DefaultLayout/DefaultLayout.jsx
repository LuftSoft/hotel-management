import { useEffect, useState } from "react";

import ScrollTopButton from "../../components/ScrollTopButton";
import Header from "../Header";
import Footer from "../Footer";

export default function DefaultLayout({ children }) {
	const [gotoTop, setGoToTop] = useState(false);
	useEffect(() => {
		const handleScroll = () => {
			if (window.scrollY > 300) {
				setGoToTop(true);
			} else {
				setGoToTop(false);
			}
		};
		window.addEventListener("scroll", handleScroll);
		return () => {
			window.removeEventListener("scroll", handleScroll);
		};
	}, []);
	return (
		<>
			{/* overlay for mobile menu */}
			{/* open mobile menu */}
			{/* mobile menu */}
			<Header />
			{children}
			{/* Footer */}
			<Footer />
			{/* Search Modal */}
			{/* Scroll Top */}
			{gotoTop && <ScrollTopButton />}
		</>
	);
}
