import SidebarUser from "../../components/SidebarUser/SidebarUser";

export default function AccountPage() {
	return (
		<div className="bg-light">
			<div className="Container">
				<div className="d-flex flex-column">
					<div className="d-flex gap-4 my-3">
						<SidebarUser />
						<div className="flex-grow-1"></div>
					</div>
				</div>
			</div>
		</div>
	);
}
