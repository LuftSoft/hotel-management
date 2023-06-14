export default function UserMenu() {
	return (
		<div className="d-flex">
			<div className="dropdown">
				<button
					type="button"
					className="btn btn-link text-decoration-none dropdown-toggle"
					data-bs-toggle="dropdown"
					aria-expanded="false">
					<img src="/img/user-avatar.png" alt="user-avatar" style={{ width: 40, height: 40 }} />
					<span className="ms-2">user name</span>
				</button>
				<div className="dropdown-menu dropdown-menu-end">
					<a href="#" className="dropdown-item">
						Tài khoản
					</a>
					<a href="#" className="dropdown-item">
						Đăng xuất
					</a>
				</div>
			</div>
		</div>
	);
}
