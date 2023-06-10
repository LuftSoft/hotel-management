import Header from "../Header";
import Footer from "../Footer";

export default function DefaultLayout({ children }) {
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
		</>
	);
}
